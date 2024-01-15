## Docker Game Server Factory

### About

This project contains C# WebApi for creating multiple game servers and connecting to them using join codes. Servers can be created by clients from different platforms, including WebGL. The project uses Sql Server to store join codes and information about created servers.
Each game server instance is open on ports 7777 and 7778 so that WebGL clients can connect with other platforms.

### Usage
```bash
docker compose build

# Starts all services except gameserver
docker compose up -d --scale gameserver=0
```

This command will launch WebApi and SqlServer. Game servers should be started using REST requests to WebApi.
The `.env` file contains the `PROJECT_NAME` and `NETWORK` constants. If you want to have several different games on the same virtual machine, make unique constants for each game.

### API

| API | Description | Request body | Response body
|-----|-------------|--------------|----------------|
| `POST /createserver` | Creates a server instance| None | Join Code
| `POST /connect/{joinCode}` | Provides server info by code | None | Server in JSON format
| `GET /connect` | Get all servers | None | Array of servers in JSON format
| `POST /removeserver/{id}` | Delete server | None | None

> [!NOTE]  
> `POST /removeserver/{id}` can only be called from [Private Address Space](https://www.rfc-editor.org/rfc/rfc1918). This is to ensure that servers cannot be deleted from the outside. 
This method can be called by your created server, for example, when all players are disconnected.

### Build from Source
In Unity, go to Build Settings, change the platform to Dedicated Server (Linux) and build. The server build should be in the `Server/build` folder and named `serverbuild.x86_64`.

### Usage for Mirror
[This](https://github.com/ArtemVetik/com.agava.mirrorserverapi) package contains a custom NetworkManager that automatically removes a server when needed, and a C# API for creating and connecting to servers.
