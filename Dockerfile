FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

COPY ./src/Ingredients.Web/ ./Ingredients.Web/

#RUN dotnet restore ./Ingredients.Web/Ingredients.Web.csproj
RUN dotnet publish ./Ingredients.Web/Ingredients.Web.csproj -c Release -o out

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
ARG CI_COMMIT_TAG=1.0.0

COPY --from=build-env /app/Ingredients.Web/out .

RUN sed -i -e 's/0.0.0/'$CI_COMMIT_TAG'/g' ./api-manifest.json;

EXPOSE 80

ENTRYPOINT ["dotnet", "Ingredients.Web.dll"]