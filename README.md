- [1. Introduction](#1-introduction)
  - [1.1 Why?](#11-why)
  - [1.2 What?](#12-what)
- [2. Setup](#2-setup)
- [3. Building](#3-building)
- [4. Deploying](#4-deploying)

# 1. Introduction

## 1.1 Why?

Because everyone I know is currently working from home, we need a way to tell our families when we can 
and can't be interrupted. Since that generally lines up with my MS Teams presence information, I decided 
to see if I could bridge the gap between Microsoft Graph and the Hubitat Maker API with an Azure Function.

## 1.2 What?

This is an Azure Function that polls MS Teams presence information for one's AAD account and then tells 
a cloud-enabled Hubitat Elevation device to change the color of a IoT RGB light bulb.

# 2. Setup

1. Register an application in AAD in the Azure Portal

2. Add the following values into your user secrets for this app:

```dotnet user-secrets set appId "Application (client) ID from above"```
```dotnet user-secrets set tenantId "Directory (tenant) ID from above"```
```dotnet user-secrets set scopes "User.Read;Calendars.Read"```
```dotnet user-secrets set userId "The ObjectID of the AAD account you want to see presence info for."```


# 3. Building

1. Clone the repo
2. ```dotnet build```
3. profit?

# 4. Deploying

1. Magical Azure Incantation 1
2. Magical Azure Incantation 2

# 5. Acknowledgements

- Lots of the Microsoft.Graph beta code was adapted from the excellent work over at [https://github.com/isaacrlevin/PresenceLight/](https://github.com/isaacrlevin/PresenceLight/)