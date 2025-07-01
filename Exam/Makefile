MIGRATION_NAME ?= Init
PROJECT ?= ./HotelService.Infrastructure/HotelService.Infrastructure.csproj
STARTUP_PROJECT ?= ./HotelService.API/HotelService.API.csproj
WEBAPP_PROJECT = ./HotelService.API/HotelService.API.csproj

run-webapp:
	@echo "Запуск веб-приложения"
	@dotnet run --project $(WEBAPP_PROJECT)

add:
	@echo "Создание миграции: $(MIGRATION_NAME)"
	@dotnet ef migrations add $(MIGRATION_NAME) --project $(PROJECT) --startup-project $(STARTUP_PROJECT)

remove:
	@echo "Удаление последней миграции"
	@dotnet ef migrations remove --project $(PROJECT) --startup-project $(STARTUP_PROJECT)

apply:
	@echo "Применение миграций"
	@dotnet ef database update $(MIGRATION_NAME) --project $(PROJECT) --startup-project $(STARTUP_PROJECT)

revert:
	@echo "Откат до миграции: $(MIGRATION_NAME)"
	@dotnet ef database update $(MIGRATION_NAME) --project $(PROJECT) --startup-project $(STARTUP_PROJECT)

revert-all:
	@echo "Откат миграций"
	@dotnet ef database update 0 --project $(PROJECT) --startup-project $(STARTUP_PROJECT)

.PHONY: run-webapp add remove apply revert revert-all

