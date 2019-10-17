MIGRATION = AppDbContext
# List of RIDs can be found here: 
# https://docs.microsoft.com/en-us/dotnet/core/rid-catalog
RID = linux-x64
###############################################

### EXECUTABLES ###############################
DOTNET = dotnet
###############################################

# ---------------------------------------------


PHONY = _make
_make: deps build cleanup

PHONY += deps
deps:
	$(DOTNET) restore

PHONY += build
build:
	$(DOTNET) build

PHONY += publish
publish:
	$(DOTNET) publish \
		-c Release \
		-r $(RID) \
		--self-contained true

PHONY += test
test:
	$(DOTNET) test

PHONY += run
run:
	$(DOTNET) run

PHONY += cleanup
cleanup:
	$(DOTNET) clean

PHONY += migrations
migrations:
	$(DOTNET) ef migrations add $(MIGRATION)

PHONY += help
help:
	@echo "Available targets:"
	@echo "  #          - creates binary .dll files"
	@echo "  cleanup    - cleans up bin and obj directories"
	@echo "  deps       - ensure dependencies are installed"
	@echo "  migrations - creates migrations using entity framework"
	@echo "  publish    - compiles to self contained binaries"
	@echo "  run        - compiles and runs the application"
	@echo "  test       - run tests"
	@echo ""


.PHONY: $(PHONY)