# Billogram .NET-Integration
This is a simple wrapper for the Billogram API (https://billogram.com/api/documentation)

Right now it only supports Customers and Invoices. Take a look in the tests to understand how it works. It is basically two repositories right now (CustomerRepository and InvoiceRepository)

To authenticate to Billogram you need to sett appSettings in your config file:
```
<appSettings>
    <add key="billogramApiServiceUrl" value="https://sandbox.billogram.com/api/v2/"/>
    <add key="billogramApiUserId" value="[userid]"/>
    <add key="billogramApiPassword" value="[password]"/>
  </appSettings>
```

The project has dependencies to:
```
<packages>
  <package id="Newtonsoft.Json" version="9.0.1" targetFramework="net461" />
  <package id="NLog" version="4.3.10" targetFramework="net461" />
</packages>
```
