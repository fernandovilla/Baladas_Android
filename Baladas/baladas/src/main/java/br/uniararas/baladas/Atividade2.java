package br.uniararas.baladas;

import android.app.Activity;
import android.os.Bundle;
import android.widget.Button;

/**
 * Created by Fernando on 12/04/2014.
 */
public class Atividade2 extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        Button botao = new Button(this);
        botao.setText("Vai muleque!");
        setContentView(botao);
    }

    @Override
    protected void onStart() {
        super.onStart();
    }

    @Override
    protected void onRestart() {
        super.onRestart();
    }

    @Override
    protected void onResume() {
        super.onResume();
    }

    @Override
    protected void onStop() {
        super.onStop();
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
    }

    @Override
    protected void onPause() {
        super.onPause();
    }
}
