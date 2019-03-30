FROM microsoft/dotnet:sdk AS build-env
WORKDIR /Ingredients.Test

ENTRYPOINT ["dotnet", "test", "/p:CollectCoverage=true", "/p:CoverletOutputFormat=json"]