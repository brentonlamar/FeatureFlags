# Gets Version from AsseblyInfo.cs updates nuspec's version
if [ "$TRAVIS_BRANCH" == "master" ] && [ "$TRAVIS_PULL_REQUEST" == false ]; then
	# Need to make this work => declare $versionNumber = "1.0.${TRAVIS_BUILD_NUMBER}.0"
	echo "========================================================="
	echo "Updating nuspec version"

	sed -i "s|\(<version>\)[^<>]*\(</version>\)|\11.0.${TRAVIS_BUILD_NUMBER}.0\2|g" ./FeatureFlags/FeatureFlags.nuspec

	echo "========================================================="

	echo "Starting to pack the NuGet packages"

	nuget pack ./FeatureFlags/FeatureFlags.nuspec 

	echo "Finished packing."

	echo "Deploying to public hosting server"	

	nuget push ./FeatureFlags.1.0.${TRAVIS_BUILD_NUMBER}.0.nupkg $PublicNugetAPIKey -Source https://www.nuget.org/api/v2/package
fi