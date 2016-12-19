using UnityEngine;
using System.Collections;
using Infrastructure.Core.Login;

public class GameManager : MonoBehaviour {

    public LoginController loginController;
    public LobbyController lobbyController;

	// Use this for initialization
	void Start () {
        Debug.Log("Game Manager Starting");
        if ( PlayerPrefs.HasKey("sessionId") )
        {
            Debug.Log("Session Id Found");
            LoginService loginService =  Infrastructure.Base.Application.Application.getInstance().serviceManager.get<LoginService>() as LoginService;
            loginService.LoginAsGuest(PlayerPrefs.GetString("sessionId"));
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
