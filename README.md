- [1. Introduction](#1-introduction)
  - [1.1 Why?](#11-why)
  - [1.2 What?](#12-what)
- [2. Setup](#2-setup)
    - [NB: No real light standards exists](#nb-no-real-light-standards-exists)
- [3. Building](#3-building)
- [4. Acknowledgements](#4-acknowledgements)

# 1. Introduction

## 1.1 Why?

Because everyone I know is currently working from home, we need a way to tell our families when we can 
and can't be interrupted. Since that generally lines up with my MS Teams presence information, I decided 
to see if I could bridge the gap between Microsoft Graph and the Hubitat Maker API with a .Net Core 
Console App (and possibly an Azure Function).

## 1.2 What?

This is a .Net Core 3.1 CLI that polls MS Teams presence information for an AAD/MS Teams account and then 
tells a cloud-enabled Hubitat Elevation API to change the color of a IoT RGB light bulb.

# 2. Setup

1. Register an application in AAD in the Azure Portal.  See [this tutorial](https://docs.microsoft.com/en-us/graph/tutorials/dotnet-core?tutorial-step=2)

2. Setup the Hubitat [Maker API](https://docs.hubitat.com/index.php?title=Maker_API).

3. Edit the appsettings.json or user secrets with values appropriate to your setup. Settings file structure:
  ````javascript
{
  "Azure": {
    "UserId": "00000000-0000-0000-0000-000000000000",   // The AAD Object Id of the user you want presence information for.
    "TenantId": "00000000-0000-0000-0000-000000000000", // The Tenant Id of the AAD organization you want to query.
    "Scopes": "Presence.Read;Presence.Read.All",        // The explicit rights your AAD app registration will use to query for presence information.
    "AppId": "00000000-0000-0000-0000-000000000000"     // The application or client Id of the AAD app registration.
  },
  "Hubitat": {
    "Id": "{integer}",                                  // Hubitat cloud id (only if not using a local network address)
    "Token": "00000000-0000-0000-0000-000000000000",    // The Hubitat Maker API access token 
    "NameOrIp": "192.168.1.15",                         // The local DNS name or IP address of your Hubitat.
    "DeviceName": "On-Air Light"                        // The Label (Name) of the Hubitat device you want to send command to.
    "StatusMap": {                                      // Dictionary mapping MS Teams presence values to commands to send to the IoT light.
      "Available": "presetGreenFade",
      "AvailableIdle": "presetGreenFade",
      "Away": "presetYellowFade",
      "BeRightBack": "",
      "Busy": "presetRedFade",
      "BusyIdle": "presetRedFade",
      "DoNotDisturb": "presetRedFade",
      "Offline": "",
      "PresenceUnknown": ""
    }
  }
}
  ````

### NB: No real light standards exists

The actual implementation details of the commands your IoT light bulb may require are not standardized, so you may have to make changes to the flow or commands issued through the Hubitat in order to get this to work.  The lights I used in this project were a ["MagicLight Smart Light Bulb (60w Equivalent), A19 7W Multicolor 2700k-6500k Dimmable WiFi LED Bulb, Compatible with Alexa Google Home Siri IFTTT"](https://www.newegg.com/magiclight-mlight-wf-7w-3p-wifi/p/35B-0065-00010?Item=9SIAJ09AFU1364)

# 3. Building

1. Clone the repo
2. Compile. Run:
  ````console
C:\> dotnet build
  ````
3. Generate binaries. Run:
  ````console
C:\> dotnet publish
  ````
4. Run the binary.  When prompted, visit https://microsoft.com/devicelogin and enter the displayed code to grant use of your AAD account to query for MS Teams presence information.

# 4. Acknowledgements
- This [blog post](https://developer.microsoft.com/en-us/graph/blogs/microsoft-graph-presence-apis-are-now-available-in-public-preview/?WT.mc_id=-blog-scottha)
- This [wonderful framework](https://github.com/CamSoper/puppet)
- This [other thing](https://github.com/isaacrlevin/PresenceLight)