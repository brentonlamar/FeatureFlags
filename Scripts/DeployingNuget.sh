# Gets Version from AsseblyInfo.cs updates nuspec's version

CURRENT_TAG=`git describe --exact-match --abbrev=0 --tags`

if [ "$TRAVIS_BRANCH" == $ReleaseFromBranch ] && [ "$TRAVIS_PULL_REQUEST" == false ] && [ $CURRENT_TAG != "" ]; then
	
	PREVIOUS_TAG=`git describe HEAD^1 --abbrev=0 --tags`
	GIT_HISTORY=`git log --no-merges --format="- %s" $PREVIOUS_TAG..HEAD`

	if [[ $PREVIOUS_TAG == "" ]]; then
  		GIT_HISTORY=`git log --no-merges --format="- %s"`
	fi

	echo "Current Tag: $CURRENT_TAG"
	echo "Previous Tag: $PREVIOUS_TAG"
	echo "Release Notes:
	$GIT_HISTORY"

	# Thanks to @tylanpince: https://gist.github.com/taylanpince/fd6b291d283f5ff09fe8


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