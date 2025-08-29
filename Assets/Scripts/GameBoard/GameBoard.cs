using UnityEngine;

public class GameBoard : MonoBehaviour
{
    private Camera _camera;

    private Vector2 ConvertToWorldPoint(Vector2 viewPortPoint)
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
        
        return  _camera.ViewportToWorldPoint(viewPortPoint);
    }
    
    public Vector2 GetRandomPointOutsideBoard()
    {
        float x = Random.Range(-0.1f, 0.1f);
        float y = Random.Range(-0.1f, 0.1f);
        if (x >= 0) x += 1;
        if (y >= 0) y += 1;
        Vector2 randomPoint = new Vector2(x, y);

        return ConvertToWorldPoint(randomPoint);
    }
}