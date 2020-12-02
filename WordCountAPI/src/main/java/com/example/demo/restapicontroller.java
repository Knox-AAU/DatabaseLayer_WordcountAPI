package com.example.demo;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.http.MediaType;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.util.Collection;
import java.util.List;
import java.util.Map;
import java.sql.*;
import javax.sql.*;
@SpringBootApplication
@RestController
public class restapicontroller {
    @Autowired
    JdbcTemplate jdbc;

    @PostMapping(value = "/wordCountData", consumes = {MediaType.APPLICATION_JSON_VALUE})
    public String insert(@RequestBody String input) throws Exception {
        List<Map<String, Object>> rows = jdbc.queryForList("SELECT * FROM wordlist");
        //System.out.println(rows);
        JSONParser parser = new JSONParser();
        JSONObject object = (JSONObject) parser.parse(input);



//            JSONObject jsonlist = (JSONObject) object.get("words");
        JSONArray jsonarray = (JSONArray) object.get("words");
        String articletitle = (String) object.get("articletitle");
        String filepath = (String) object.get("filepath");

        if(articletitle == null)
        {
            throw new Exception("Article title must not be null");
        }
        if(filepath == null)
        {
            throw new Exception("Filepath must not be null");
        }


            insertfiledata("filelist(filepath, articletitle, totalwordsinarticle)",
                    (String) object.get("filepath"),
                    (String) object.get("articletitle"),
                    Integer.parseInt(object.get("totalwordsinarticle").toString()));



        for (int i = 0; i < jsonarray.size(); i++) {
            JSONObject jsonEntry = (JSONObject) jsonarray.get(i);
            String entryWord = (String) jsonEntry.get("word");
            if(entryWord == null)
            {
                throw new Exception("word must not be null");
            }

            long entryAmount = (long) jsonEntry.get("amount");
            System.out.println(entryWord + " " + entryAmount);
            insertworddata("wordlist(wordname)", entryWord);

            insertappearsdata("appearsin(amount, filepath, articletitle, wordname)",
                    (int) entryAmount, (String) object.get("filepath"), (String) object.get("articletitle"), entryWord);
        }




        //word = (String) object.get("word");
        System.out.println(" word: " + object.get("word") + '\n'
                + " filepath: " + object.get("filepath") + '\n'
                + " articletitle: " + object.get("articletitle") + '\n'
                + " totalwordsinarticle: " + object.get("totalwordsinarticle"));




        /*
        if(object.get("amount") != null && object.get("word") != null && object.get("filepath") != null && object.get("articletitle") != null)
        {
            //insert data; amount, wordname, filepath, articletitle
            insertappearsdata("appearsin(amount, filepath, articletitle, wordname)",
                    Integer.parseInt(object.get("amount").toString()),
                    (String) object.get("filepath"),
                    (String) object.get("articletitle"),
                    (String) object.get("word")
            );
        } */
        return "done";
    }
    public int insertfiledata(String tableref, String filepath,
                              String articletitle, int totalwordsinarticle) throws SQLException{
        String sql = String.format("INSERT INTO %s" + "VALUES ('%s','%s',%d) ON CONFLICT DO NOTHING",
                tableref,filepath, articletitle, totalwordsinarticle);
        jdbc.update(sql);
        return 1;
    }

    public int insertworddata(String tableref, String word) throws SQLException {
        String sql = String.format("INSERT INTO %s " + "VALUES ('%s') ON CONFLICT DO NOTHING",tableref, word);
        jdbc.update(sql);
        return 1;
    }

    public int insertappearsdata(String tableref, int amount,
                                 String filepath, String filename, String word) throws SQLException {
        String sql = String.format("INSERT INTO %s" + "VALUES (%d, '%s', '%s', '%s') ON CONFLICT DO NOTHING", tableref, amount, filepath, filename, word);
        jdbc.update(sql);
        return 1;
    }
}