- [1. Introduction](#1-introduction)
  - [1.1 Why?](#11-why)
  - [1.2 What?](#12-what)
- [2. Building](#2-building)
- [3. Deploying](#3-deploying)

# 1. Introduction

## 1.1 Why?

Because everyone I know is currently working from home, we need a way to tell our families when we can 
and can't be interrupted. Since that generally lines up with my MS Teams presence information, I decided 
to see if I could bridge the gap between Microsoft Graph and the Hubitat Maker API with an Azure Function.

## 1.2 What?

This is an Azure Function that polls MS Teams presence information for one's AAD account and then tells 
a cloud-enabled Hubitat Elevation device to change the color of a IoT RGB light bulb.

# 2. Building

1. Clone the repo
2. ```dotnet build```
3. profit?

# 3. Deploying

1. Magical Azure Incantation 1
2. Magical Azure Incantation 2