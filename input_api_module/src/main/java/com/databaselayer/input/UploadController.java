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
import org.apache.jena.util.FileManager;
import org.apache.jena.util.FileManagerImpl;
import org.rdfhdt.hdt.enums.RDFNotation;
import org.rdfhdt.hdt.exceptions.ParserException;
import org.rdfhdt.hdt.hdt.HDT;
import org.rdfhdt.hdt.hdt.HDTManager;
import org.rdfhdt.hdt.options.HDTSpecification;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.*;

import java.io.*;

@SpringBootApplication
@RestController
public class UploadController {
    static final String pathToFiles = "/home/christoffer/Documents/hdt/";
    static final String HDTDataPath =  pathToFiles + "swdf.hdt";
    static final String RDFDataPath = pathToFiles + "temp.nt";
    static final String RDFOutput =    pathToFiles + "output.nt";
    static final String libPath = "'hdt-lib.jar:/home/christoffer/Documents/hdt/lib/*'";
    //Måske husk at slette index, når filen er blevet erstattet /todo
    // Takes a string (.ttl) and recompresses the HDT with the new information and restarts the server.
    @PostMapping(value = "/update", consumes = {MediaType.APPLICATION_FORM_URLENCODED_VALUE})
    public String uploadFile(@RequestBody String inputTriples) throws IOException, InterruptedException {
        // Decompress HDT

        Process p = Runtime.getRuntime().exec("java -server -Xmx1024M -classpath '/home/christoffer/Documents/hdt/hdt-lib.jar:/home/christoffer/Documents/hdt/lib/*' org.rdfhdt.hdt.tools.HDT2RDF /home/christoffer/Documents/hdt/swdf.hdt /home/christoffer/Documents/hdt/decompressed.rdf");

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

        // QueryBuilder to access Decompressed HDT
        Model model = ModelFactory.createDefaultModel();
        model.read(new FileInputStream(RDFDataPath),null,"N-TRIPLES");
        org.apache.jena.query.Dataset dataset = DatasetFactory.assemble(model);
        UpdateRequest updateQuery = UpdateFactory.create("INSERT DATA { " + inputTriples + " }");
        UpdateProcessor updater = UpdateExecutionFactory.create(updateQuery, dataset);


        // Query updateQuery = QueryFactory.create("INSERT DATA { " + inputTriples + " }" );
        // QueryExecution queryExecutor = QueryExecutionFactory.create(updateQuery, model);
        try {
            updater.execute();
        }
        catch (Exception e){e.printStackTrace();}
        // Save QueryBuilder as RDF
        RDFDataMgr.write(new FileOutputStream(RDFOutput), dataset, RDFFormat.TTL );

        return "Updated";
    }

    @PostMapping("/commit")
    public String commit() throws IOException, ParserException {
        // Compress HDT
        HDT hdt = HDTManager.generateHDT(
                RDFOutput,         // Input RDF File
                "",          // Base URI
                RDFNotation.parse("TTL"), // Input Type
                new HDTSpecification(),   // HDT Options
                null              // Progress Listener
        );

        // Stop server

        // Switch old HDT with new HDT

        // Start server


        return "Committed";
    }
}
