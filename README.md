DotNetOpenAuth.OSChinaOAuth2
============================

DotNetOpenAuth OAuth2 Client for OSChina

OSChina Reference: http://www.oschina.net/openapi

## Setup

1. Install this library from [NuGet](https://www.nuget.org/packages/DotNetOpenAuth.OSChinaOAuth2),
                                  
        PM> Install-Package DotNetOpenAuth.OSChinaOAuth2
 
 ... or compile from source and add a reference

2. Register the client to OAuthWebSecurity.

 ```csharp
 var client = new OSChinaOAuth2("yourClientId", "yourClientSecret");
 OAuthWebSecurity.RegisterClient(client, "OSChina");
 ```
 
 ## Disclaimer

I don't work for Google, Microsoft, or DNOA.  This is released under the [MIT](LICENCE.txt) licence.  Do what you want with it.

And, Thank you mj1856.
