# BlogWebApi
Simple blog platform in .NET core
# Example Request Body(Json)
{
    "title": "some title here",
    "description": "some description",
    "body": "somebody once told me",
    "tagList": ["tag1", "tag2", "tag3"]
}

Features:
 - Consumes and produces application/json
  - Database created on launch
  - Slug automatically generated from title
  - Returns a wrapped object or a list of objects
  - Initial seed provided
  - To get post by slug, just add the slug to the url
  - To filter posts by tag, add ?tag=yourtaghere to url


