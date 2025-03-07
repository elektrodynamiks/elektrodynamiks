file_data = File.read("16-in.txt").split
#p file_data.count

test = File.read("16-run.txt").split
puzzle = File.read("17-in.txt").split

test = "
2413432311323
3215453535623
3255245654254
3446585845452
4546657867536
1438598798454
4457876987766
3637877979653
4654967986887
4564679986453
1224686865563
2546548887735
4322674655533"
test = test.split
$run = test
# $run = file_data
p [$x_wide = $run.first.length - 1, $y_wide = $run.length - 1]

$maze = $run.map { |arr| arr.chars }

def show
  $maze.each do |m|
    p m
  end
end

show

$start = [0, 0]
$end = [$x_wide, $y_wide]

$costs_of_route = Set.new
