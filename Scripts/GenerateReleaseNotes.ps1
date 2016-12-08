$currentTag = git describe --exact-match --abbrev=0 --tags
$previousTag = git describe HEAD^1 --abbrev=0 --tags
git log --no-merges --format="- %s" $PREVIOUS_TAG..HEAD | Out-File ../ReleaseNotes/release-notes-$currentTag.md
