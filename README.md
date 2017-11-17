ReportStyleModifier
===================

Introduction
------------

The goal of this product is to centralized some customizable
parts of any SSRS report.

The previous document present some parts:
* How it's works
* Technical contents
* Into the wood
* Current limitation
* Future development

How it's works
--------------

Everything into a SSRS report can be accessed by an Expression.
The easiest way to feed data to an expression is to use a dataset.
The idea is to produce a dataset into the SSRS report and change
every element of the report you want managed with the following
expression:

`<FontFamily>=First(Fields!HEADER_Font_FontFamily.Value, "dsReportStyle")</FontFamily>`

The dataset will be create by a stored procedure ``up_ReportStyle_by_name_and_parameter``
It take 2 parameters:
* ``@report_name``
* ``@report_parameter`` (could be null)

You can identify the style you want use for this report by the report name and
another build-in element.

Of course it's boring to replace everywhere the style value by the expression,
so you found into this repository an command line program ``ReportStyleModifier``
who will do that for you. You just need some manually actions:

1. Create the dataset will send style values
2. For each element you want manage the style, add on the name of element (Texbox indeed at now)
   the style name you want use. (See above for the list of style contained into the basic database).
3. Run the program and give the report file path and the name of the dataset.

To manage style, you need:
1. Create some entry into the ``ReportStyle`` table
2. For each pair of element / style you want customize, create an entry into The
   table ``ReportStyleElement``
3. Assign a style to a pair of _report name_ and _report parameters_

If you change any style or any assignation, the next run of you report will be changed.

Technical Content
-----------------

The current solution contains 4 Visual Studio projects:

1. ReportStyleDatabase:
   This database with everything you need to manage styles.
   In addition some elements will be insert at first deployment.
   Six identifier styles are inserted:
   * For the **main** section:
     * HEADER
     * SUBHEAD
   * For **table**:
     * MAINTITLE
     * SUBTITLE
     * STANDARD
     * TOTAL
   For each identifier style six element are inserted:
   * font color
   * font family
   * font size
   * font style
   * font weight
   * background color

2. ReportStyleModifier
   It's the program will modify the report description file.

3. ReportStyleModifierTest
   Unit test for the modifier program

4. ReportStyleReport
   Sample report project only based on the database produce by the first
   project to demonstrate how produce a customized project.

Into the Wood
-------------

The Element name need follow a strict format:
[**Place** _ ]**Style name** _ **Style part** _ **Style tag name**

**Place** can get 2 values:
* **MAIN** (if you don't use 4 parts but only 3, it means you want use **MAIN**)
* **TABLE** (Every Textbox selected need to be contained into a table)

**Style name** can get every value you want

**Style part** can take 2 values, for now:
* **Font** element related about the font of course
* **Background** for assign the background of the Textbox

** Style tag name** depend strongly of the style part:
* For **Font** can take 5 values, for now:
  * color
  * family
  * size
  * style
  * weight
* For **Background** only one value: color

About the program, it not need any database connection, it read the report file
and get all style used by the dataset as Field of dataset.
After that, it will select all Textbox with the good name into the good place,
and so add all Style tag into the xml code.

Current Limitation
------------------

There some limitation:

* A very few number of unit test are developed, so some issues can still exist
  on code
* No Char can be assign by the current code but, maybe it's compatible, just not
  enough time to test it
* Create Element, or style. Assignation of report to style need to be done
  by SQL queries

Future development
------------------

**Idea 1**: Create a web app to manage Styles and assignation

**Idea 2**: Add Element for Chart

**Idea 3**: Check how assign also other displayable elements as size of
table cells for example.


