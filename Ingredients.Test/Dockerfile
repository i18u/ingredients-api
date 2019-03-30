FROM microsoft/dotnet:sdk AS build-env
WORKDIR /Ingredients.Test

ENTRYPOINT ["dotnet", "Ingredients.Test.dll", "/p:CollectCoverage=true", "/p:CoverletOutputFormat=json"]