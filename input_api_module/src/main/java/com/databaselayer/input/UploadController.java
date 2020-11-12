package com.databaselayer.input;

import org.apache.jena.query.*;
import org.apache.jena.rdf.model.Model;
import org.apache.jena.rdf.model.ModelFactory;
import org.apache.jena.riot.RDFDataMgr;
import org.apache.jena.riot.RDFFormat;
import org.apache.jena.update.UpdateExecutionFactory;
import org.apache.jena.update.UpdateFactory;
import org.apache.jena.update.UpdateProcessor;
import org.apache.jena.update.UpdateRequest;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

import java.io.*;

@SpringBootApplication
@RestController
public class UploadController {
    // pathToFiles should match /etc/fuseki/databases/hdt on server
    static final String pathToFiles = "/home/christoffer/Documents/hdt";
    static final String HDTDataFileName =  "database.hdt";

    static final String RDFDataPath = "/home/christoffer/Documents/hdt/temp.ttl";
    static final String RDFFileName = "temp.ttl";
    static final String RDFUpdatedFileName = "updated_temp.ttl";
    static final String RDFUpdatedPath =    pathToFiles + "/updated_temp.ttl";

    // NewHDTFileName should match the old HDT filename
    static final String NewHDTFileName =   "database.hdt";

    //Måske husk at slette index, når filen er blevet erstattet /todo
    // Takes a string (.ttl and recompresses the HDT with the new information and restarts the server.
    @PostMapping(value = "/update", consumes = {MediaType.APPLICATION_JSON_VALUE})
    public String uploadFile(@RequestBody String inputTriples) throws IOException, InterruptedException {
        System.out.println("inputTriples input:\n" + inputTriples);

        // Model
        Model model;

        // Check if temp file exists
            // Decompress HDT
         /*   String[] cmd = {"bash","-c", "java -server -Xmx1024M -classpath 'hdt-lib.jar:lib/*' org.rdfhdt.hdt.tools.HDT2RDF " + HDTDataFileName + " " + RDFFileName};
            Process p = Runtime.getRuntime().exec(cmd, null, new File(pathToFiles));

            BufferedReader stdInput = new BufferedReader(new
                    InputStreamReader(p.getInputStream()));

            BufferedReader stdError = new BufferedReader(new
                    InputStreamReader(p.getErrorStream()));

            // Read the output from the command
            System.out.println("Here is the standard output of the command:\n");
            String s = null;
            while ((s = stdInput.readLine()) != null) {
                System.out.println(s);
            }

            // Read any errors from the attempted command
            System.out.println("Here is the standard error of the command (if any):\n");
            while ((s = stdError.readLine()) != null) {
                System.out.println(s);
            }

            p.waitFor();
            active = true;

            model = ModelFactory.createDefaultModel();
            model.read(new FileInputStream(RDFDataPath),null,"TTL");
          */

        // Operate on updated temp file
        model = ModelFactory.createDefaultModel();
        model.read(new FileInputStream(RDFUpdatedPath),null,"TTL");
        org.apache.jena.query.Dataset dataset = DatasetFactory.create(model);
        UpdateRequest updateQuery = UpdateFactory.create("INSERT DATA { " + inputTriples + " }");
        UpdateProcessor updater = UpdateExecutionFactory.create(updateQuery, dataset);


        // Query updateQuery = QueryFactory.create("INSERT DATA { " + inputTriples + " }" );
        // QueryExecution queryExecutor = QueryExecutionFactory.create(updateQuery, model);
        try {
            updater.execute();
        }
        catch (Exception e){e.printStackTrace();}
        // Save QueryBuilder as RDF

        System.out.println("Model\n");
        System.out.println(model);

        RDFDataMgr.write(new FileOutputStream(RDFUpdatedPath), dataset.getDefaultModel(), RDFFormat.TURTLE );

        return "Updated";
    }



    @PostMapping("/commit")
    public String commit() throws IOException {
        // Generate new HDT
        String[] cmdConvertToHDT = {"bash","-c", "java -server -XX:NewRatio=1 -XX:SurvivorRatio=9 -Xmx1024M -classpath 'hdt-lib.jar:lib/*' org.rdfhdt.hdt.tools.RDF2HDT " + HDTDataFileName + " " + NewHDTFileName};
        Process p2 = Runtime.getRuntime().exec(cmdConvertToHDT, null, new File(pathToFiles));

        // Restart server
        String[] cmdRestart = {"bash","-c", "sudo systemctl restart fuseki"};
        Process p3 = Runtime.getRuntime().exec(cmdRestart);


        return "Committed";
    }
}
