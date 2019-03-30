FROM microsoft/dotnet:sdk AS build-env
WORKDIR /Ingredients.Test

ENTRYPOINT ["dotnet", "Ingredients.Web.dll", "/p:CollectCoverage=true", "/p:CoverletOutputFormat=json"]