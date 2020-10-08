
package com.example.demo;
import java.util.*;

import org.apache.jena.query.*;
import org.apache.jena.rdfconnection.RDFConnectionFuseki;
import org.apache.jena.rdfconnection.RDFConnectionRemoteBuilder;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ResponseBody;
import javax.xml.transform.Result;


public class fuseki_controller {
    @GetMapping("/dinmor")
    @ResponseBody
    public ResultSet query(String input) {
        RDFConnectionRemoteBuilder builder = RDFConnectionFuseki.create()
                .destination("http://localhost:3030/ds/");
        Query query =  QueryFactory.create(input);

        RDFConnectionFuseki conn = (RDFConnectionFuseki)builder.build();
        QueryExecution qExec = conn.query(input);
        ResultSet rs = qExec.execSelect();

        return rs;
    }

    public void update(String input){
        RDFConnectionRemoteBuilder builder = RDFConnectionFuseki.create()
                .destination("http://localhost:3030/ds/");
        RDFConnectionFuseki conn = (RDFConnectionFuseki)builder.build();
        conn.update(input);

    }

}


//skrive selecct, lave kode itl at håndtere det (dvs konvertere resultater til en liste)