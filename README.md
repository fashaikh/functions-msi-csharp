# FunctionApp with MSI in CSharp

This is code for an [Azure Function](https://azure.microsoft.com/en-us/services/functions/) that is deployed to run using a Managed Service Identity (MSI).

Once installed it creates a Function App linked to an MSI. If you go ahead and change the access of the MSI to particular resources, then it is able to talk to ARM and return the results


* Note: Tread carefully with this sample as your ARM resource data will be exposed via an admin url now. Of course you can use this as a template and  change the code to do any ARM related processing in the background

## Quick Deploy to Azure

[![Deploy to Azure](http://azuredeploy.net/deploybutton.svg)](https://azuredeploy.net/)

### To run in Azure, run the ARM Template found in azuredeploy.json and then fill in the app settings with the following values:
- FunctionApp Name : The name of the function App you want to create

In azure portal find the MSI that was created (search based on FunctionApp name) and update its permissions based on your needs

### To run locally open in VSCode and fill in the following values in the local.settings.json file 
```
{
    "IsEncrypted": false,
    "Values": {
    "AzureWebJobsStorage": "",
    "FUNCTIONS_WORKER_RUNTIME": "",
    "BlobStorageConnectionString": "",
    "BlobStorageAccountName":"",
    "BlobStorageContainerName": "",
    "BlobStorageBlobName": "",
    "tenantId" : "",
    "AzureServicesAuthConnectionString":"RunAs=App;AppId=<>;TenantId=<>;AppKey=<>"
    }
}
```
