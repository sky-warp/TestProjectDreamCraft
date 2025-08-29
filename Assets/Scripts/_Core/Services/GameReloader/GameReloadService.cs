using UnityEngine.SceneManagement;

public class GameReloadService
{
    private DisposableService _disposableService;
    
    [DInjection]
    public void Construct(DisposableService disposableService)
    {
        _disposableService = disposableService;
    }
    
    public void Reload()
    {
        _disposableService.DisposeAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}