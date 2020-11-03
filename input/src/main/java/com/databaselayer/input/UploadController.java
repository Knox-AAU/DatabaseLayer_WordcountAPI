package com.databaselayer.input;
import com.hp.hpl.jena.query.*;
import org.apache.jena.rdf.model.Model;
import org.apache.jena.rdf.model.ModelFactory;
import org.apache.jena.riot.RDFDataMgr;
import org.apache.jena.util.FileManager;
import org.apache.jena.util.FileManagerImpl;
import org.rdfhdt.hdt.enums.RDFNotation;
import org.rdfhdt.hdt.exceptions.ParserException;
import org.rdfhdt.hdt.hdt.HDT;
import org.rdfhdt.hdt.hdt.HDTManager;
import org.rdfhdt.hdt.options.HDTSpecification;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.web.bind.annotation.*;

import java.io.*;

@SpringBootApplication
@RestController
public class UploadController {

    // Takes a string (.ttl) and recompresses the HDT with the new information and restarts the server.
    @PostMapping("/update")
    public String uploadFile(@RequestBody String file) throws FileNotFoundException {
        // Decompress HDT


        // QueryBuilder to access Decompressed HDT
        InputStream inputStream = new FileInputStream(
                new File("/etc/fuseki/data.ttl"));

        // Model model = FileManagerImpl.loadModelInternal("/etc/fuseki/data.ttl");
        // Model newModel = ModelFactory.createDefaultModel() ;
        
        // Save QueryBuilder as RDF


        return "Updated";
    }

    @PostMapping("/commit")
    public String commit() throws IOException, ParserException {
        // Compress HDT
        HDT hdt = HDTManager.generateHDT(
                "test.ttl",         // Input RDF File
                "Non-existant",          // Base URI
                RDFNotation.parse("ntriples"), // Input Type
                new HDTSpecification(),   // HDT Options
                null              // Progress Listener
        );

        // Stop server

        // Switch old HDT with new HDT

        // Start server


        return "Committed";
    }
}
