# Ruby
input = <<~EXAMPLE
xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
EXAMPLE



class DataManipulation
  attr_reader :mul_instructions, :mul_conditionals, :multiplication_result
  def initialize(input)
    @mul_instructions = []
    @mul_conditionals =[]
    @multiplication_result = 0
    data_manipulation(input)
    main()
  end

  def data_manipulation(input)
    regexp = /mul\(\d+\,\d+\)/i
    print corrupted_mem = input.scan(regexp)
    
    corrupted_mem.each.map do |mem|
      @mul_instructions << mem.scan(/\d+/i).map { |a| a.to_i}
    end
    
  # part II
  # looking for mul instruction between don't() and do()
  
  end

  def main
    @mul_instructions.each do |mul_instruction|
    @multiplication_result += mul_instruction[0] * mul_instruction[1]
    end
    puts @multiplication_result
  end

end

# Part 1
puts "example"
DataManipulation.new(input)

puts "puzzle"
# puzzle_input = File.read("day03_input.txt")
# DataManipulation.new(puzzle_input)
#
# Part 2
#
puts "puzzle - Part 2"
puzzle_input = File.read("day03_part2.txt")
DataManipulation.new(puzzle_input)