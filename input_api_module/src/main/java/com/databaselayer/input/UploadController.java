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
    static final String pathToFiles = "/etc/fuseki/databases/hdt";
    static final String HDTDataFileName =  "database.hdt";
    static final String RDFUpdatedPath =    pathToFiles + "/updated_temp.ttl";

    // NewHDTFileName should match the old HDT filename
    static final String NewHDTFileName =   "database.hdt";

    // TODO: Delete index file if it doesn't work.
    // Takes a string (.ttl and recompresses the HDT with the new information and restarts the server.
    @PostMapping(value = "/update", consumes = {MediaType.APPLICATION_JSON_VALUE})
    public String uploadFile(@RequestBody String inputTriples) throws IOException, InterruptedException {
        System.out.println("inputTriples input:\n" + inputTriples);

        // Model
        Model model;

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
    public String commit() throws IOException, InterruptedException {
        // Generate new HDT
        String[] cmdDeleteOldHdt = {"bash","-c", "rm " + pathToFiles + "/database.hdt"};
        Process p0 = Runtime.getRuntime().exec(cmdDeleteOldHdt, null, new File(pathToFiles));

        // Generate new HDT
        String[] cmdDeleteIndex = {"bash","-c", "rm " + pathToFiles + "/database.hdt.index.v1-1"};
        Process p1 = Runtime.getRuntime().exec(cmdDeleteIndex, null, new File(pathToFiles));

        // Generate new HDT
        String[] cmdConvertToHDT = {"bash","-c", "java -server -XX:NewRatio=1 -XX:SurvivorRatio=9 -Xmx1024M -classpath '/opt/inputlayer-app/converter/hdt-lib.jar:/opt/inputlayer-app/converter/lib/*' org.rdfhdt.hdt.tools.RDF2HDT -rdftype turtle " + RDFUpdatedPath + " " + pathToFiles + "/" + NewHDTFileName};
        Process p2 = Runtime.getRuntime().exec(cmdConvertToHDT, null, new File(pathToFiles));

        // Print out errors from p2 when generating new HDT
        BufferedReader stdInput = new BufferedReader(new
                InputStreamReader(p2.getInputStream()));

        BufferedReader stdError = new BufferedReader(new
                InputStreamReader(p2.getErrorStream()));

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

        p2.waitFor();


        // Restart server
        String[] cmdRestart = {"bash","-c", "systemctl restart fuseki"};
        Process p3 = Runtime.getRuntime().exec(cmdRestart);


        return "Committed";
    }
}
