FROM microsoft/dotnet:2.2-sdk AS build-env

# Copy everything else and build
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out ./
CMD dotnet WebjetTestServer.dll