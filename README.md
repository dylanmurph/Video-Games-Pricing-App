# Video Games Pricing App - SOA CA1

# Game Deal Finder

A price comparison site for PC games. Helps find the cheapest online store to buy PC games, and provides a list of alternative stores. 

## How it works

The app combines two free web apis: **RAWG** and **CheapShark**

- RAWG is used for gathering games lists, used in search, ratings, release year, descriptions
- CheapShark is the API for gathering pricing for the games and redirect links to storepages, it is also used to get the small thumbnail images seen in the search results.

## References
- RAWG API: https://rawg.io/apidocs
- CheapShark API: https://apidocs.cheapshark.com/
- Blazor documentation: https://learn.microsoft.com/aspnet/core/blazor/
- HTTPClient: https://learn.microsoft.com/aspnet/core/fundamentals/http-requests
- Custom exceptions: https://learn.microsoft.com/dotnet/standard/exceptions/how-to-create-user-defined-exceptions
- Bootstrap documentation: https://getbootstrap.com/docs/5.3/
- Bootstrap classes: https://bootstrapshuffle.com/classes
- Bootstrap enable dark mode: https://www.w3schools.com/bootstrap5/bootstrap_dark_mode.php
