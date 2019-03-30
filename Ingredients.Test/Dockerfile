FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

RUN ls
RUN cd Ingredients.Test/

ENTRYPOINT ["dotnet", "Ingredients.Web.dll", "/p:CollectCoverage=true", "/p:CoverletOutputFormat=json"]