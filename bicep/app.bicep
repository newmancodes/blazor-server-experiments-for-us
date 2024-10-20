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

@description('The log analytics workspace id to connect diagnostics configuration to.')
param logAnalyticsWorkspaceId string

// @description('The application insights connection string to use for application logging.')
// param applicationInsightsConnectionString string

resource appServicePlan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: 'asp-${environmentName}-${locationShortForm}-${solutionSuffix}'
  location: location
  properties: {
    reserved: true
  }
  sku: {
    name: 'F1'
  }
  tags: standardTags
  kind: 'linux'
}

resource appServicePlanLogsToAnalytics 'Microsoft.Insights/diagnosticSettings@2017-05-01-preview' = {
  scope: appServicePlan
  name: 'aspLogToAnalytics'
  properties: {
    workspaceId: logAnalyticsWorkspaceId
    logs: [
      {
        category: 'AllLogs'
        enabled: true
      }
    ]
    metrics: [
      {
        category: 'AllMetrics'
        enabled: true
      }
    ]
  }
}
