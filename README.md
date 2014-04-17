﻿Google Analytics API v3 .Net
============================

### How to get unique visitors for a Google Analytics profile using .Net

The code for getting unique visitors (and other metrics) from Google Analytics API is pretty simple - however it requires some setup on the Google side to get the code running.

##Walkthrough:
Great answer on SO:
http://stackoverflow.com/a/19299981/3215104

##Tools
Find the profile ids using Google Analytics 
http://ga-dev-tools.appspot.com/explorer/

## GOTCHAS
* You must give the service account e-mail generated for you read access to your GA account.
* If the helper is run in IIS 7+ you might get the following exception 
  ```
  "An internal error occurred."Exception Type":"System.Security.Cryptography.CryptographicException"
  ```
  Make sure that the app pool > advanced settings > process model > load user profile is set to true.
