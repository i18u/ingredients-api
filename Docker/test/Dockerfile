FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app-test

COPY ./Docker/test/run-tests.sh ./
COPY ./ingredient-api.sln ./
COPY ./Ingredients.Web/ ./Ingredients.Web/
COPY ./Ingredients.Cache/ ./Ingredients.Cache/
COPY ./Ingredients.Test/ ./Ingredients.Test/

RUN dotnet restore ./ingredient-api.sln

RUN ["chmod", "+x", "/app-test/run-tests.sh"]
ENTRYPOINT ["/app-test/run-tests.sh"]