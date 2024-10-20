# Blazor Server Experiments for US

[![CI/CD Solution Management](https://github.com/newmancodes/blazor-server-experiments-for-us/actions/workflows/main.yml/badge.svg)](https://github.com/newmancodes/blazor-server-experiments-for-us/actions/workflows/main.yml)

## RBAC Role Assignments

The service principal used to deploy must receive the following RBAC Role Assignments:

| RBAC Role | Scope |
|-|-|
| Contributor | Subscription |
| Azure Deployment Stack Owner | Subscription |

## GitHub Environments

Required Environments:

- devl

## GitHub Environment Variables and Secrets

| Name | Type | Purpose |
|-|-|-|
| AZURE_DEPLOYMENT_APP_CLIENT_ID | Secret | The client id from the Entra app registration used during deployment |
| AZURE_DEPLOYMENT_APP_TENANT_ID | Secret | The tenant id containing the Entra app registration used during deployment |
| AZURE_DEPLOYMENT_APP_SUBSCRIPTION_ID | Secret | The subscription id to target during deployment |
