package com.example.demo;

import org.apache.jena.query.*;
import org.apache.jena.rdfconnection.RDFConnectionFuseki;
import org.apache.jena.rdfconnection.RDFConnectionRemoteBuilder;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import java.util.*;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import static org.junit.jupiter.api.Assertions.*;

class fuseki_controllerTest2 {

    @BeforeEach
    void setUp() {

    }

    @AfterEach
    void tearDown() {

    }
    @Test
    void isServerUp() {
        fuseki_controller controller = new fuseki_controller();
        ResultSet output = controller.query("SELECT ?x ?y ?z WHERE { ?x ?y ?z}");
        System.out.println(output);
        assertNotEquals(output, null);
    }

    @Test
    void doesServerContainData(){
        fuseki_controller controller = new fuseki_controller();
        ResultSet output = controller.query("SELECT ?x ?y ?z WHERE { ?x ?y ?z}");
        //System.out.println(output.next().getLiteral("z").toString());

        assertNotEquals(output.next().get("x").toString(), null);
    }

    @Test
   void insertdata()
    {
        fuseki_controller controller = new fuseki_controller();

        controller.update("PREFIX dc: <http://purl.org/dc/elements/1.1/>\n" +
                "INSERT DATA\n" +
                "{ <http://example/book3> dc:title    \"A new book\" ;\n" +
                "                         dc:creator  \"A.N.Other\" .\n" +
                "}");
        ResultSet output = controller.query("PREFIX dc: <http://purl.org/dc/elements/1.1/>\n" +
                "SELECT ?x WHERE {?x dc:title \"A new book\" ;\n}");
        assertNotEquals(output.next().get("x").toString(), null);

        controller.update("PREFIX dc: <http://purl.org/dc/elements/1.1/>\n" +
                "DELETE WHERE\n" +
                "{ <http://example/book3> ?x ?y};");

    }

}