# Kiwi's Co-Op Mod for Half-Life: Alyx

<a href="https://github.com/TeamPopplio/KCOM"><img align="left" width="256" src="https://i.imgur.com/qIIjxCs.png"></a>

[![Releases](https://img.shields.io/github/v/tag/teampopplio/kcom?label=release)](https://github.com/teampopplio/kcom/releases)

[![Downloads](https://img.shields.io/github/downloads/teampopplio/kcom/total)](https://github.com/teampopplio/kcom/releases)

[![Workshop Subscribers](https://img.shields.io/steam/subscriptions/2739356543?label=workshop%20subscribers)](https://steamcommunity.com/sharedfiles/filedetails/?id=2739356543)

[![Discord](https://img.shields.io/discord/738131767944282183?label=discord)](https://discord.gg/3X3teNecWs)

[![License](https://img.shields.io/github/license/teampopplio/kcom)](https://github.com/TeamPopplio/KCOM/blob/main/LICENSE)

[![Contributors](https://img.shields.io/github/contributors/teampopplio/kcom)](https://github.com/teampopplio/kcom/graphs/contributors)

--------

### Kiwi's Co-Op Mod for Half-Life: Alyx (KCOM) is a cooperative experience for Half-Life: Alyx.

## Features

- Simultaneous gameplay with up to 16 players
- VR hands are synced across clients
- Physics objects are synced across clients
- Trigger outputs are synced across clients
- Custom addon (gamemode/plugin) support
- Support for Workshop maps
- Discord rich presence
- Addon contents within GitHub

## Installation

- Subscribe to the [Workshop Addon](https://steamcommunity.com/sharedfiles/filedetails/?id=2739356543) on Steam
- Download the [latest release](https://github.com/TeamPopplio/KCOM/releases) from GitHub
- Extract the contents of the zip file to a safe place (e.g. a new folder on your desktop)
- Set the following launch options for Half-Life: Alyx in Steam: `-console -vconsole -language english`
- Launch the game
- Open KCOM using `KiwisCoOpMod.exe`
- Follow instructions within the KCOM application
- KCOM is ready!

## Connecting to a Server
- Follow the [instructions](#installation) to install KCOM
- Click on the "Client" tab in the KCOM application
- Check the "Enabled" box to allow your client to connect
- Set the IP address to the *public* IP address of the remote server
- Set the port to the port of the remote server
- *(Optional)* If provided, set the password to the password of the remote server
- Set a username for your client
- Click "Connect"
- KCOM will connect to the remote server and start playing!

## Hosting a Server

- Follow the [instructions](#installation) to install KCOM
- *(Optional)* Follow the [client instructions](#connecting-to-a-server) to create a listen server
	- Note: Non-listen servers are not fully developed yet, please follow the above instructions for now
- Click on the "Server" tab in the KCOM application
- Check the "Enabled" box to host a server
- *(Optional)* Set a password for the server, make sure to provide it to peers
- Type in a map name to load initially (use `mp_kiwitest` for testing)
- Ensure that the port is forwarded, try UPnP mapping via "File" > "Forward Port via UPnP"
- Click on the "Start" button to start the server
- KCOM is now hosting a server! You can now provide your *public* IP address to peers

## Help & Support

Join the [Discord](https://discord.gg/3X3teNecWs) for support between users and developers.
