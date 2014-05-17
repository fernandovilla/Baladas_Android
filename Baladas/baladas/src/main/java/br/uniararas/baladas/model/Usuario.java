package br.uniararas.baladas.model;

/**
 * Created by Fernando on 15/05/2014.
 */
public class Usuario {

    private int ID;
    private String Nome;
    private int Genero;
    private int Tipo;



    public int getID() {
        return ID;
    }
    public void setID(int ID) {
        this.ID = ID;
    }

    public String getNome() {
        return Nome;
    }
    public void setNome(String nome) {
        Nome = nome;
    }

    public int getGenero() {
        return Genero;
    }
    public void setGenero(int genero) {
        Genero = genero;
    }

    public int getTipo() {
        return Tipo;
    }
    public void setTipo(int tipo) {
        Tipo = tipo;
    }

    public static Usuario Parse(String jsonResponse)
    {
        return new Usuario();
    }

    public Usuario(){

    }
}
