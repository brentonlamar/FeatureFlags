#Updates AssemblyInfo.cs version.
#Major and Minor have to be updated manually.

#FeatureFlags

echo "Adjusting Contracts AssemblyVersion from:"
tail -2 $TRAVIS_BUILD_DIR/FeatureFlags/Properties/AssemblyInfo.cs  | head -1

# Updates AssemblyVersion for Nuget creation.
sed -ri "s/AssemblyVersion\(\"([0-9]+.[0-9]+.)[0-9]+.[0-9]+\"\)/AssemblyVersion(\"\1${TRAVIS_BUILD_NUMBER}.0\")/g" ./FeatureFlags/Properties/AssemblyInfo.cs

echo "Adjusted Contracts AssemblyVersion to:"
tail -2 $TRAVIS_BUILD_DIR/FeatureFlags/Properties/AssemblyInfo.cs  | head -1