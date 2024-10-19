targetScope = 'subscription'

@description('The Azure region to deploy resources into.')
param location string

@description('The short form of the Azure region e.g. uksouth -> ukst.')
@minLength(4)
@maxLength(4)
param locationShortForm string

@description('The environment classification.')
@allowed(['devl', 'stag', 'prod'])
param environmentName string

// beus - Blazor Experiments for US
var solutionSuffix = 'beus'

var standardTags = {
  owner: 'steve.newman@digital'
}

resource resourceGroup 'Microsoft.Resources/resourceGroups@2024-07-01' = {
  location: location
  name: 'rg-${environmentName}-${locationShortForm}-${solutionSuffix}'
  tags: standardTags
}

module monitoring 'monitoring.bicep' = {
  name: 'monitoring'
  scope: resourceGroup
  params: {
    location: location
    locationShortForm: locationShortForm
    environmentName: environmentName
    solutionSuffix: solutionSuffix
    standardTags: standardTags
  }  
}
