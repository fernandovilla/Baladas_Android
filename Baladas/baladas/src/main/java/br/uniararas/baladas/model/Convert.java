package br.uniararas.baladas.model;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;

/**
 * Created by Fernando on 15/05/2014.
 */
public class Convert {
    public static String InputStreamToString(InputStream inputStream) throws IOException {
        if (inputStream == null){ return ""; }

        ByteArrayOutputStream outputStream  = new ByteArrayOutputStream();
        byte[] buffer = new byte[1024];
        int bufferSize = 0;

        //Read the inputStream content and write it into the outputStream
        while((bufferSize = inputStream.read(buffer)) > 0)
        {
            outputStream.write(buffer, 0, bufferSize);
        }

        //Convert the outputStream content to String;
        String textValue = new String(outputStream.toByteArray());

        return textValue;
    }
}
