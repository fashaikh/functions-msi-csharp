# FunctionApp with MSI in CSharp

This is code for an [Azure Function](https://azure.microsoft.com/en-us/services/functions/) that is deployed to run using a Managed Service Identity (MSI).

Once installed it creates a Function App linked to an MSI. If you go ahead and change the access of the MSI to particular resources, then it is able to talk to ARM and return the results

* Note: Tread carefully with this sample as your ARM resource data will be exposed via an admin url now. Of course you can use this as a template and  change the code to do any ARM related processing in the background

## Quick Deploy to Azure

[![Deploy to Azure](http://azuredeploy.net/deploybutton.svg)](https://azuredeploy.net/)
### Step 1 Deploy FunctionApp Template ![Step 1](https://user-images.githubusercontent.com/2650941/53583547-a4c29880-3b36-11e9-808c-0b2937a43691.PNG)

### To run in Azure, run the ARM Template found in azuredeploy.json and then fill in the app settings with the following values:
- FunctionApp Name : The name of the function App you want to create

In azure portal find the MSI that was created (search in Subscriptions View Access Control blase based on FunctionApp name) and update its permissions based on your needs. For multiple subscriptions you will have to repeat the step for each subscription

### Step 2 Update Permissions : ![Step 2 Update Permissions](https://user-images.githubusercontent.com/2650941/53583563-aa1fe300-3b36-11e9-804f-b84243378aab.jpg)

### Step 3 Execute Function : ![Step 3 Execute Function ](https://user-images.githubusercontent.com/2650941/53583986-83ae7780-3b37-11e9-947f-5c5ce6c6e999.png)

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
