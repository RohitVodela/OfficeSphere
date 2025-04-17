# Unit Testing Plan for EmployeeController

This document outlines a comprehensive unit testing plan for the `EmployeeController` class in the OfficeSphere application. The plan is designed to achieve over 80% code coverage by testing all HTTP methods with both positive and negative scenarios.

## Test Project Setup

1. Create an XUnit test project named `OfficeSphere.Tests`
2. Add the following NuGet packages:
   - xunit
   - xunit.runner.visualstudio
   - Microsoft.NET.Test.Sdk
   - FluentAssertions (for more readable assertions)
   - Moq (for mocking dependencies if needed)
   - coverlet.collector (for code coverage)
3. Add a reference to the main OfficeSphere project

## Test Class Structure

```csharp
public class EmployeeControllerTests : IDisposable
{
    private readonly EmployeeController _controller;
    private static readonly object _lockObject = new object();

    public EmployeeControllerTests()
    {
        // Reset the employees list before each test to ensure test isolation
        ResetEmployeesList();
        _controller = new EmployeeController();
    }

    public void Dispose()
    {
        // Clean up after each test
        ResetEmployeesList();
    }

    private void ResetEmployeesList()
    {
        // Use reflection to access and reset the private static _employees field
        lock (_lockObject)
        {
            var field = typeof(EmployeeController).GetField("_employees", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            
            field.SetValue(null, new List<Employee>
            {
                new Employee { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Department = "IT" },
                new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Department = "HR" }
            });
        }
    }

    private Employee CreateTestEmployee(int id = 3, string role = "Developer", string department = "Testing")
    {
        return new Employee
        {
            Id = id,
            FirstName = "Test",
            LastName = "User",
            Email = $"test.user{id}@example.com",
            Department = department,
            Role = role,
            YearsOfExperience = 5
        };
    }
}
```

## Test Methods by HTTP Endpoint

### 1. GetEmployees() Tests

**Positive scenarios:**
- `GetEmployees_ReturnsAllEmployees` - Verify that all employees are returned
- `GetEmployees_ReturnsCorrectEmployees` - Verify the returned employees have the correct properties

### 2. GetEmployee(int id) Tests

**Positive scenarios:**
- `GetEmployee_WithValidId_ReturnsEmployee` - Verify that an employee with a valid ID is returned
- `GetEmployee_WithValidId_ReturnsCorrectEmployee` - Verify the returned employee has the correct properties

**Negative scenarios:**
- `GetEmployee_WithInvalidId_ReturnsNotFound` - Verify NotFound is returned for non-existent ID

### 3. PostEmployee(Employee employee) Tests

**Positive scenarios:**
- `PostEmployee_AddsEmployeeToList` - Verify the employee is added to the list
- `PostEmployee_ReturnsCreatedAtAction` - Verify the correct status code and location header
- `PostEmployee_ReturnsCreatedEmployee` - Verify the returned employee matches the input

### 4. PutEmployee(int id, Employee employee) Tests

**Positive scenarios:**
- `PutEmployee_WithValidId_UpdatesEmployee` - Verify employee properties are updated
- `PutEmployee_WithValidId_ReturnsNoContent` - Verify the correct status code

**Negative scenarios:**
- `PutEmployee_WithInvalidId_ReturnsNotFound` - Verify NotFound for non-existent ID

### 5. DeleteEmployee(int id) Tests

**Positive scenarios:**
- `DeleteEmployee_WithValidId_RemovesEmployee` - Verify employee is removed from list
- `DeleteEmployee_WithValidId_ReturnsNoContent` - Verify the correct status code

**Negative scenarios:**
- `DeleteEmployee_WithInvalidId_ReturnsNotFound` - Verify NotFound for non-existent ID

### 6. CalculateSalary(Employee employee) Tests

**Positive scenarios:**
- `CalculateSalary_WithManager_ReturnsCorrectSalary` - Test with Manager role
- `CalculateSalary_WithDeveloper_ReturnsCorrectSalary` - Test with Developer role
- `CalculateSalary_WithDesigner_ReturnsCorrectSalary` - Test with Designer role
- `CalculateSalary_WithDefaultRole_ReturnsCorrectSalary` - Test with other roles

### 7. UpdateSalary(Employee employee) Tests

**Positive scenarios:**
- `UpdateSalary_WithValidId_UpdatesEmployeeSalary` - Verify salary is updated
- `UpdateSalary_WithValidId_ReturnsNoContent` - Verify the correct status code

**Negative scenarios:**
- `UpdateSalary_WithInvalidId_ReturnsNotFound` - Verify NotFound for non-existent ID

### 8. SearchEmployeesByName(string name) Tests

**Positive scenarios:**
- `SearchEmployeesByName_WithMatchingName_ReturnsEmployees` - Verify matching employees are returned
- `SearchEmployeesByName_WithMatchingLastName_ReturnsEmployees` - Verify matching by last name works
- `SearchEmployeesByName_WithPartialName_ReturnsEmployees` - Verify partial matching works
- `SearchEmployeesByName_WithMatchingName_CalculatesSalaries` - Verify salaries are calculated

**Negative scenarios:**
- `SearchEmployeesByName_WithNonMatchingName_ReturnsNotFound` - Verify NotFound when no matches

### 9. GetEmployeesByRole(string role) Tests

**Positive scenarios:**
- `GetEmployeesByRole_WithMatchingRole_ReturnsEmployees` - Verify matching employees are returned
- `GetEmployeesByRole_WithMatchingRole_CalculatesSalaries` - Verify salaries are calculated
- `GetEmployeesByRole_WithCaseInsensitiveRole_ReturnsEmployees` - Verify case-insensitive matching

**Negative scenarios:**
- `GetEmployeesByRole_WithNonMatchingRole_ReturnsNotFound` - Verify NotFound when no matches

### 10. GetEmployeesByDepartment(string department) Tests

**Positive scenarios:**
- `GetEmployeesByDepartment_WithMatchingDepartment_ReturnsEmployees` - Verify matching employees are returned
- `GetEmployeesByDepartment_WithMatchingDepartment_CalculatesSalaries` - Verify salaries are calculated
- `GetEmployeesByDepartment_WithCaseInsensitiveDepartment_ReturnsEmployees` - Verify case-insensitive matching

**Negative scenarios:**
- `GetEmployeesByDepartment_WithNonMatchingDepartment_ReturnsNotFound` - Verify NotFound when no matches

## Test Implementation Example

Here's an example of how to implement one of the test methods:

```csharp
[Fact]
public void GetEmployee_WithValidId_ReturnsEmployee()
{
    // Arrange
    int validId = 1;
    
    // Act
    var result = _controller.GetEmployee(validId);
    
    // Assert
    var actionResult = Assert.IsType<ActionResult<Employee>>(result);
    var returnValue = Assert.IsType<Employee>(actionResult.Value);
    Assert.Equal(validId, returnValue.Id);
}
```

## Handling Static List

The `_employees` list in the controller is static, which can cause tests to interfere with each other. This is addressed by:

1. Using a test helper method to reset the list to its initial state before each test
2. Using IDisposable to set up and tear down test data
3. Using a lock to prevent race conditions in parallel test execution

## Code Coverage Strategy

To achieve >80% code coverage:

1. Test all branches in conditional statements (if/else, switch)
2. Test all possible return paths in each method
3. Use code coverage tools (coverlet) to identify untested code
4. Focus on testing business logic in the controller methods

## Test Data

Test data factories are used to generate Employee objects with different roles and properties:

```csharp
private Employee CreateTestEmployee(int id = 3, string role = "Developer")
{
    return new Employee
    {
        Id = id,
        FirstName = "Test",
        LastName = "User",
        Email = $"test.user{id}@example.com",
        Department = "Testing",
        Role = role,
        YearsOfExperience = 5
    };
}
```

## Implementation Timeline

1. Set up the test project and add required packages
2. Implement test fixture setup and helper methods
3. Implement tests for basic CRUD operations (Get, Post, Put, Delete)
4. Implement tests for specialized methods (CalculateSalary, UpdateSalary)
5. Implement tests for search/filter methods (SearchByName, ByRole, ByDepartment)
6. Run code coverage analysis and add tests for any missed code paths
7. Refactor and optimize tests as needed
