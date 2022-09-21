using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piezas : MonoBehaviour
{
    public int coordenadaX;
    public int coordenadaY;

    public TipoMovimiento tipoMovimiento;
    public TipoFicha tipoFicha;

    public bool yaSeEjecuto = true;

    public int timeMovement = 2;

    public AnimationCurve curve;

    public Board board;

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Moverpieza(new Vector3((int)transform.position.x, (int)transform.position.y + 1, 0), timeMovement);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Moverpieza(new Vector3((int)transform.position.x - 1, (int)transform.position.y, 0), timeMovement);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Moverpieza(new Vector3((int)transform.position.x + 1, (int)transform.position.y, 0), timeMovement);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Moverpieza(new Vector3((int)transform.position.x, (int)transform.position.y - 1, 0), timeMovement);
        }*/
    }
    public void Coordenada(int cambioX, int cambioY)
    {
        coordenadaX = cambioX;
        coordenadaY = cambioY;
    }
   public void Moverpieza(int x, int y, float timeMovement)
   {
        if (yaSeEjecuto == true)
        {
            StartCoroutine(MovePiece(new Vector3(x,y,0), timeMovement));
        }
   }
    IEnumerator MovePiece(Vector3 endPosition, float timeMovement)
    {
        yaSeEjecuto = false;
        bool llegoAlPunto = false;
        Vector3 startPosition = transform.position;
        float timepoTranscurrido = 0;

        while (!llegoAlPunto)
        {
            if (Vector3.Distance(transform.position, endPosition) < 0.01f)
            {
                llegoAlPunto = true;
                yaSeEjecuto = true;
                transform.position = new Vector3((int)endPosition.x, (int)endPosition.y);
                board.PiezaPosicion(this,(int)endPosition.x ,(int)endPosition.y);
                break;
            }

            float t = timepoTranscurrido / timeMovement;

            switch(tipoMovimiento)
            {
                case TipoMovimiento.lineal:
                    t = curve.Evaluate(t);
                    break;
                case TipoMovimiento.suavizado:
                    //movimiento suavizado
                    t = t * t * (3 - 2 * t);
                    break;
                case TipoMovimiento.entrada:
                    t = 1 - Mathf.Cos(t * Mathf.PI * .5f);
                    break;
                case TipoMovimiento.salida:
                    t = Mathf.Sin(t * Mathf.PI * .5f);
                    break;
                case TipoMovimiento.MasSuavizado:
                    t = t * t * t * t * (t * (t * 6 - 15) + 10);
                    break;
            }
            timepoTranscurrido += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return new WaitForEndOfFrame();
        }
    }
    public enum TipoMovimiento
    {
        lineal,
        entrada,
        suavizado,
        salida,
        MasSuavizado,
    }
    public enum TipoFicha
    {
        monstruo1,
        monstruo2,
        monstruo3,
        monstruo4,
        monstruo5,
        monstruo6,
    }
}
