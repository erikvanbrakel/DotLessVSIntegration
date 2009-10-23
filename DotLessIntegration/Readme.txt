v0.0.1 of the .Less/{Less} visual studio integration.

Prerequisites for development are:
    - Visual Studio 2008 (Standard)
    - Visual Studio 2008 SDK (http://www.microsoft.com/downloads/details.aspx?familyid=59EC6EC3-4273-48A3-BA25-DC925A45584D&displaylang=en)

The project at the current state is more or less a rip-off from the default package project, with a bit of tinkering to show all text in blue.
We need to figure out how to fit our parser into this.

Useful Information:
- Main work we have to do is in the IScanner implementation (DotLessScanner). The integration classes work a bit iffy, because it's COM-interop. Expect integers as return types and
  ByRef parameters.