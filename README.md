
# Vehicle Rental System

This project implements a simple command-line interface (CLI) application for managing vehicle rentals. It calculates rental costs, insurance fees, and generates an invoice based on specific business rules for different types of vehicles.

## Approach

### Classes and Structure

The solution uses object-oriented programming principles to model different types of vehicles and manage rental transactions through an `Invoice` class. Here's an overview of the key components:

-   **Vehicle Class Hierarchy**:
    
    -   **Vehicle**: Base class containing common properties like `Brand`, `Model`, and `Value`.
    -   **Car**: Derived from `Vehicle`, includes additional property `SafetyRating`.
    -   **Motorcycle**: Derived from `Vehicle`, includes additional property `RiderAge`.
    -   **CargoVan**: Derived from `Vehicle`, includes additional property `DriverExperience`.
-   **Invoice Class**:
    
    -   Manages the rental calculation logic and generates an invoice.
    -   Calculates rental costs based on rental period and type of vehicle.
    -   Computes insurance costs with adjustments based on vehicle specifics (safety rating, rider age, driver experience).
    -   Applies discounts for early returns according to business rules.

### Business Rules

The application adheres to specific business rules for calculating rental and insurance costs:

-   **Rental Costs**:
    
    -   $20/day for cars if rented for a week or less, $15/day otherwise.
    -   $15/day for motorcycles if rented for a week or less, $10/day otherwise.
    -   $50/day for cargo vans if rented for a week or less, $40/day otherwise.
-   **Insurance Costs**:
    
    -   Calculated as a percentage of the vehicle's value:
        -   0.01% for cars.
        -   0.02% for motorcycles.
        -   0.03% for cargo vans.
    -   Adjustments based on safety ratings, rider age, and driver experience.
-   **Early Return Discounts**:
    
    -   Applied if the vehicle is returned before the reserved end date:
        -   50% discount on rental cost for remaining days.
        -   Insurance charges only for the days used.

### Usage

The application is designed to be run from the command line. Users can input customer and vehicle details directly into the code to generate invoices automatically. Each invoice includes detailed information such as rental period, costs breakdown, and total amounts due.
