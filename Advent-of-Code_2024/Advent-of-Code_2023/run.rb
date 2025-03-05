file_data = File.read("19-in.txt")
#p file_data.count

test = "
px{a<2006:qkq,m>2090:A,rfg}
pv{a>1716:R,A}
lnx{m>1548:A,A}
rfg{s<537:gd,x>2440:R,A}
qs{s>3448:A,lnx}
qkq{x<1416:A,crn}
crn{x>2662:A,R}
in{s<1351:px,qqz}
qqz{s>2770:qs,m<1801:hdj,R}
gd{a>3333:R,R}
hdj{m>838:A,pv}

{x=787,m=2655,a=1222,s=2876}
{x=1679,m=44,a=2067,s=496}
{x=2036,m=264,a=79,s=2244}
{x=2461,m=1339,a=466,s=291}
{x=2127,m=1623,a=2188,s=1013}"

workflow, parts = test.split("\n\n")

def flow(flow)
  variable = flow.slice!(0)
  rule = flow.slice!(0)
  number = flow.match(/\d*/)[0]
  escape = flow.split(":").last
  return { variable: variable.to_sym, rule: rule, number: number.to_i, escape: escape.to_sym }
end

def workflow(lines)
  flow = lines.split("{")
  key = flow.first.to_sym
  flow = flow.last.split(",")
  processes = flow
  escape = flow.last.split("}").first.to_sym
  processes.delete_at(-1)
  conditions = []
  processes.each do |proc|
    conditions << flow(proc)
  end
  return { key => conditions << escape }
end

# create hash for functions definition from workflow
$processes = {}
workflow.split.each do |flow|
  # in{s<1351:px,qqz}
  # {in: [[:s, "<", 1351,:px]],'qqz']
  $processes.update workflow(flow)
end

p $processes[:in]
p $processes[:px]
p $processes[:qqz]
p $processes[:qs]
p $processes[:lnx]

# Node Tree. search for all nodes that lead to :A
# :in
# |   \
# :px  :qqz
# |   \
# :qkq :A
# |   |    \
# :qs :hdj :R
# | \
# :A :lnx
# keep [:in[0], :px[0],:qkq[0],:qs[0]] [:in, !:px[0]]

require "tree"
root = Tree::TreeNode.new("root")

$accepted = []

$entry_node = [:in]

def reverse_condition(sign)
  case sign
  when "<"
    return ">="
  when ">"
    return "<="
  end
end

$queue = [[root, :in]]

def process_node(node, process_id)
  process = $processes[process_id]
  conditions = {}
  condition_id = 0
  process.each do |proc|
    if proc.class == Hash
      p escape = proc[:escape]
      p proc.delete(:escape)
      p conditions = conditions.update(proc)
      node_name = process_id.to_s + condition_id.to_s
      p node_name = Tree::TreeNode.new(process_id.to_s + condition_id.to_s, conditions)
      $queue << [node_name, escape]
      p node.name
      p node.content
      node << node_name
      proc.delete(:escape)
      reverse = proc.update({ :rule => reverse_condition(proc[:rule]) })
      conditions.update reverse
    elsif proc.class == Symbol
      p node_name = process_id.to_s + condition_id.to_s
      node_name = Tree::TreeNode.new(process_id.to_s + condition_id.to_s, conditions)
      $queue << [node_name, proc]
      node << node_name
    end
    condition_id += 1
  end
end

loop do
  entry = $queue.pop
  if entry.last == :A
    $accepted << entry.first
  elsif entry.last != :R
    process_node(entry.first, entry.last)
  end
  break if $queue.empty?
end

$accepted.each { |node| p node.name }
root.print_tree

def combination(rule, range)
  range
  var = rule[:variable]
  math = rule[:rule]
  number = rule[:number]
  case math
  when "<"
    if number < range.end
      combination = (range.begin...number)
    end
  when "<="
    if number <= range.end
      combination = (range.begin..number)
    end
  when ">"
    if range.end > number
      combination = (number..range.end)
    end
  when ">="
    if range.end >= number
      combination = (number..range.end)
    end
  end
  combination = range if combination.nil?
  return { var => combination }
end

ranges = []
$accepted.each do |node|
  combination = { :a => (1..4000), :x => (1..4000), :m => (1..4000), :s => (1..4000) }
  loop do
    p node.content
    break if node.parent.nil?
    condition = node.content
    p node.name
    parameter = condition[:variable]
    range = combination(node.content, combination[parameter])
    combination.update range
    p node.name
    node = node.parent
    p node.name
    p node.content
  end
  ranges << combination
end
ranges
sum = 0

ranges.each do |range|
  total = 1
  range.values.each do |cal|
    if cal.include?(1) || cal.include?(4000)
      cal = cal.count
    else
      p cal = cal.count + 1
    end
    total *= cal
  end
  p total
  sum += total
end
p sum
456222990800000
167409079868000
