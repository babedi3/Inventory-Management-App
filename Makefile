# Project Variables
PROJECT_NAME ?= InventoryManagementApp
ORG_NAME ?= InventoryManagementApp
REPO_NAME ?= Inventory-Management-App

.PHONY: mirgrations db

mirgrations:
	cd ./InventoryManagement.Data && dotnet ef --startup-project ../InventoryManagement.Web/ migrations add $(mname) && cd ..

db:
	cd ./InventoryManagement.Data && dotnet ef --startup-project ../InventoryManagement.Web/ database update && cd ..
