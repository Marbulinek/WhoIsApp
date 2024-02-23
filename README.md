# WhoIsApp
WhoIs App is application to retrieve public data about domains, owners.
Project contains library which is able to retrieve WHOIS data.

## Technologies
    .NET 8

## Usage
```csharp
    private WhoisManager _whoisManager;
    ...
    _whoisManager = new WhoisManager();
    var result = _whoisManager.RetrieveData("lukasoft.sk");
```

Variable result contains all retrieved data, like name, company, address etc.

## Notes
Project includes also test, which could faild after some time, because domains are updating in realtime, but test not so often.