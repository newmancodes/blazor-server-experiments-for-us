targetScope = 'resourceGroup'

@description('The Azure region to deploy resources into.')
param location string

@description('The short form of the Azure region e.g. uksouth -> ukst.')
param locationShortForm string

@description('The environment classification.')
param environmentName string

@description('The suffix to apply to resource names indicating the solution.')
param solutionSuffix string

@description('The set of standard tags to apply to resources.')
param standardTags object

resource logAnalyticsWorkspace 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: 'law-${environmentName}-${locationShortForm}-${solutionSuffix}'
  location: location
  tags: standardTags
  properties: {
    sku: {
      name: 'PerGB2018'
    }
  }
}

resource applicationInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: 'apm-${environmentName}-${locationShortForm}-${solutionSuffix}'
  location: location
  tags: standardTags
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalyticsWorkspace.id
  }
}

output logAnalyticsWorkspaceId string = logAnalyticsWorkspace.id
output applicationInisghtsConnectionString string = applicationInsights.properties.ConnectionString
output applicationInsightsInstrumentationKey string = applicationInsights.properties.InstrumentationKey
