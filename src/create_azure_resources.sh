resourcename=rs-azure-function-secure-test
region=westeurope
storageName=securefunctionstorage
myappserviceplan=securefunction-hosting
functionAppName=fun-secure-trigger
azureservicebusns=azure-function-secure-test


if [ $(az group exists --name $resourcename) = false ]; then
    az group create --name $resourcename --location $region
fi

# Create an azure storage account
az storage account create \
  --name $storageName \
  --location $region \
  --resource-group $resourcename \
  --sku Standard_LRS

  # Create an App Service plan
az functionapp plan create \
  --name $myappserviceplan \
  --resource-group $resourcename \
  --location $region \
  --sku B1

# Create a Function App
az functionapp create \
  --name $functionAppName \
  --storage-account $storageName \
  --plan $myappserviceplan \
  --resource-group $resourcename \
  --functions-version 2


az servicebus namespace create \
    --name azureservicebusns \
    --resource-group $resourcename \ 
    --location $region \
    --sku Basic