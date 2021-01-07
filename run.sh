#!/bin/bash

set -e

VERSION="$1"
shift

rm -rf GetServiceName/bin
rm -rf GetServiceName/obj

sed -i -b -e 's/<PackageReference Include="Microsoft.Data.SqlClient" Version="[^"]\+" \/>/<PackageReference Include="Microsoft.Data.SqlClient" Version="'"$VERSION"'" \/>/' GetServiceName/GetServiceName.csproj

dotnet run --project GetServiceName/GetServiceName.csproj -- "$@"
