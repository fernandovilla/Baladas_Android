package br.uniararas.baladas.model;

import java.io.InputStream;
import java.util.Date;

/**
 * Created by Fernando on 15/05/2014.
 */
public class Balada {

    private int Id;
    private String Nome;
    private String Descricao;
    private String Local;
    private Date DataHotaInicio;
    private Date DataHoraTermino;
    private int ID_Promoter;
    private String Nome_Promoter;
    private boolean OpenBar;
    private double ValorHomem;
    private double ValorMulher;

    public Balada(){

    }

    public Balada Parse(String jsonResponse)
    {
        Balada balada = new Balada();
        return balada;
    }
}
