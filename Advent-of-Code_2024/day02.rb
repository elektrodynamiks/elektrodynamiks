# Ruby 3.3.6
#
input = <<~EXAMPLE
7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9
EXAMPLE

class ReportsToProgression
  attr_reader :reports, :number_of_safe_reports

  def initialize(input, tolerate_bad_level)
    @reports = []
    @number_of_safe_reports = 0
    data_manipulation(input)
    main(tolerate_bad_level)
  end

  def data_manipulation(input)
    input.each_line do |line|
      @reports << line.scan(/\w+/).map(&:to_i)
    end
  end

  def main(tolerate_bad_level)
    @reports.each do |report|
      # print report
      # puts safety_report_check?(report)
      if safety_report_check?(report) then
        safety_reports_count()
      else
        # generate the reports_with_tolerance
        reports_with_tolerance(report) if tolerate_bad_level
      end
      # puts "#--"
    end
    print "How many reports are safe: ",  @number_of_safe_reports
    puts
  end

  # PART I
  def report_to_progression(levels)
    progression = []
    levels.each_with_index.map do |level, index|
      progression << (levels[index] - levels[index - 1]) if (index > 0)
    end
    return progression
  end

  def safety_report_check?(report)
    progression = report_to_progression(report)
    # progression is increasing or decreasing
    increasing = progression.all? { |level| level > 0 }
    decreasing = progression.all? { |level| level < 0 }
    if (increasing ^ decreasing)
      # progression differ > 1 and <= 3
      at_least_one = progression.none? { |element| element.abs() == 0 }
      at_most_three = progression.none? { |element| element.abs() > 3 }
      return (at_least_one & at_most_three)
    end
    return (increasing ^ decreasing)
  end
  
  def safety_reports_count()
    @number_of_safe_reports += 1
  end

  # PART II
def  reports_with_tolerance(report)
  # puts
  for i in 0..report.length()-1 do
    input = report.clone
    input.delete_at(i)
    # check the sub-reports as created, we need only one safe.
    if safety_report_check?(input) then
      # print input,"safe"
      # puts
      safety_reports_count()
    break
    end
  end
    
  end
end


# Part I
# set tolerate_bad_level = true for PART II
tolerate_bad_level = false

puts "example"
check_the_reports = ReportsToProgression.new(input, tolerate_bad_level)
# check for the puzzle
puts "puzzle"
puzzle_input = File.read("day02_input.txt")
check_the_reports = ReportsToProgression.new(puzzle_input, tolerate_bad_level)



=begin
7 6 4 2 1 
1 2 7 8 9 
9 7 6 2 1 
1 3 2 4 5 
8 6 4 4 1 
1 3 6 7 9 
 

Data Manipulation 
from the puzzle input create a array for each report 
the array is composed of the levels separated by the spacing ' ', converted to int 
Iteration on the reports 
 
# PART 1 
Create an array of terms progression from the report 
For the report array[…value(index)...] of size n we create a array composed of the progression 
The progression array is: array[…(level[n+1]-level[n])…] for n>1 and of array.size = n-1 
 
Test the safety on the progression array. 
The report is safe if: 
if all array elements have the same sign 
(+ for increasing, - for decreasing) 
if each array elements.abs() is (> 1 and <=3) 
 
 
# PART 2 
Only for unsafe report do. 
We tolerate a single bad level: 
From the report as an ordered list of n items, 
generate n reports of size n-1  
On the each of the generated n reports 
we execute #PART 1 
the original report is safe if at least on of the sub-report is safe. 
 

Example:
Data Manipulation 
1 2 7 8 9 -> [1 2 7 8 9]  
 
Part I 
From report to progression 
[1 2 7 8 9] -> [1, 5, 1, 1]  
Tests 
 [1, 5, 1, 1]: Unsafe because 2 7 is an increase of 5. 
 
Part II 
[1 2 7 8 9] is Unsafe. 
Generate the reports of size 4 
[1 2 7 8 9] -> [[2, 7, 8, 9][1, 7, 8, 9][1, 2, 8, 9][1, 2, 7, 9][1, 2, 7, 8]] 
                Execute Part I on these reports 
                -> all unsafe

