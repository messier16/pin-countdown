CURRENT_BRANCH := $(shell git rev-parse --abbrev-ref HEAD 2>&1)
MASTER_BRANCH := master
PIPENV := pipenv
WITH_PIPENV := $(PIPENV) run

.EXPORT_ALL_VARIABLES:
PIPENV_VENV_IN_PROJECT=1

setup:
	$(PIPENV) install --dev

check-on-master:
ifneq ($(CURRENT_BRANCH),$(MASTER_BRANCH))
	$(error This operation is only available on the master branch)
else
	@echo "Working on master"
endif

increase-build:
	$(WITH_PIPENV) bumpversion build --no-tag

release-patch: check-on-master increase-build
	$(WITH_PIPENV) bumpversion patch --verbose
	git push --follow-tags

release-minor: check-on-master increase-build
	$(WITH_PIPENV) bumpversion minor --verbose
	git push --follow-tags

release-major: check-on-master increase-build
	$(WITH_PIPENV) bumpversion major --verbose
	git push --follow-tags

.PHONY: setup check-on-master release-patch release-minor release-major increase-build
