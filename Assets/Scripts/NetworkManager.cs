using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour 
{
    float btnX, btnY, btnW, btnH;
    bool refreshing = false;
	// Use this for initialization
	void Start () 
    {
        btnX = Screen.width * 0.05f;
        btnY = Screen.width * 0.05f;
        btnW = Screen.width * 0.1f;
        btnH = Screen.width * 0.1f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (refreshing)
        {
            if (MasterServer.PollHostList().Length > 0)
            {
                refreshing = false;
                Debug.Log(MasterServer.PollHostList().Length);
            }
        }
	}

    #region GUI

    void OnGUI()
    {
        if (GUI.Button(new Rect(btnX, btnY, btnW, btnH), "Start Server"))
        {
            Debug.Log("Starting Server");
            StartServer();
        }

        if (GUI.Button(new Rect(btnX, btnY * 5f, btnW, btnH), "Refresh Hosts"))
        {
            Debug.Log("Refreshing");
            RefreshHostList();
        }
    }



    #endregion

    #region Functions

    void StartServer()
    {
        Network.InitializeServer(4, 25001, !Network.HavePublicAddress());
        MasterServer.RegisterHost("Tank Deathmatch", "Yamachiche Tanks", "This is a 3rd person tank shooter");
    }

    void RefreshHostList()
    {
        MasterServer.RequestHostList("Tank Deathmatch");
        refreshing = true;
    }

    #endregion

    #region Messages

    void OnServerInitialized()
    {
        Debug.Log("Server Initialized");
    }

    void OnMasterServerEvent(MasterServerEvent mse)
    {
        if (mse == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Registered Server!");
        }
    }

    #endregion
}
