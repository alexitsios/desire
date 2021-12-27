VAR aquired_arm = false
VAR stern_door_open = false
VAR aquired_tool = false
VAR aquired_service_kit = false
VAR ship_is_sinking = false
VAR stern_visited = false
VAR funnel_visited = false

== stern_load ==
{
    - !stern_visited:
        >> settrapped true
        >> fadein 2
        Led Left "Uh... Wh- what's going on? I- I don't remember- Oh crap- M- my leg! My arm! They're gone!"
        VacuumRobot Right "So dirty. So m-m-m-much dirt. I'll nev-neve-n-never c-clean it"
        Led Left "Hey! That's my leg! You! Come over here!"
        VacuumRobot Right "C-c-clean nothing is clean n-nothing is c-c-c-c-c-c-clean"
        ~ stern_visited = true
        -> DONE
    - !ship_is_sinking && aquired_service_kit:
        >> screenshake start
        Led "No! Don't sink now!"
        >> screenshake stop
        Led "Any memories might help right now. But I need a drive for that..."
        ~ ship_is_sinking = true
        -> DONE
    - else:
        -> DONE
}


== garbage_bin ==
>> bgchange trash_on_ground
VacuumRobot "Neeeeeaaaaahhhh! M-m-more dirt! More tra-tra-trash! Nev-never finish!"
>> moveto VacuumRobot TrashBin
-> DONE

== vacuum_robot_1 ==
Led "Got it!"
Led "There, that’s a lot better! Great to be on my own two feet again. Now I need to find my arm"
VacuumRobot "Dirt! Dirt! Dirt! So much dirt! Dir dir dir dir dirt! No- no time time time time time-"
>> settrapped false
-> DONE

== vacuum_robot_2 ==
VacuumRobot "Ev- ev- everything sssssso dirty. No nada nope nein na time time time no time!"
Led "Are you alright? You’re malfunctioning pretty bad, aren’t you?"
VacuumRobot "Dirty- e-e-e-everything’s dirty. You-you’re d-d-dirty. I’m dirty- how did I g-g-get d-dirty? Dirt dirt dirt no time dirt-"
Led "My name’s Led. Can- can you remember that? Led"
VacuumRobot "L-Led. Led"
Led "That's right, Led"
VacuumRobot "Led is d-d-dirty!"
Led "Huh. He’s pretty badly damaged. Poor thing must be so confused. His voice box is definitely screwed up"
VacuumRobot "Cleandirtydirtyclean must dirty everything it’s too clean no-no that’s wrong no time no time"
Led "Hum. I wonder if I can use the trash to get him to focus"
Led "Hey- hey, look at this. Can you see this drinks can I’ve got here? Yes, that’s it. Do you know what this is?"
VacuumRobot "Tr-tr-trash, Led"
Led "Yes, yes that’s right. Well, there’s a trash can right here- would you like me to put the trash in the can?"
VacuumRobot "Yes yes y-y-yes- clean, tidy. Nice and clean please Led not dirty, don’t have time"
Led "Alright, good. Well, I can put it in there, but first, I want to ask you some questions. If you answer them, I’ll put it in the trashcan. Deal?"
VacuumRobot "D- d- d- dea- DIRT! Deal"
Led "Great, looks like I've got his attention. What should I ask?"
-> vacuum_robot_ask_table

== vacuum_robot_ask_table ==
    * [You keep saying you don't have time. Why not? What's happening?] 
        -> ask_what_happened
    * [Do- do you know what happened? Why we're both damaged?] 
        -> why_are_we_damaged
    * [Do you know where the humans are?]
        -> where_are_the_humans
    * [I don't think I have any questions] 
        -> end_discussion_with_vacuum_robot

== ask_what_happened ==
Led "You keep saying you don’t have time. Why not? What’s happening?"
VacuumRobot "The- the shhhhip. It’s g-g-going..."
Led "Going where?"
VacuumRobot "D-do-dow-DIRT! DIRTY!"
Led "Hum. Guess I’m not getting that answer. Maybe I should try something else"
-> vacuum_robot_ask_table

== why_are_we_damaged ==
Led "Do- do you know what happened? Why we’re both damaged?"
VacuumRobot "I-I-I’m a cleaning robot. No defence. No a-a-attack. Only clean. No help"
Led "What?"
VacuumRobot "I c-c-couldn’t hhhhhhelp"
Led "Right. Okay. Um… I wonder if I can ask anything else"
-> vacuum_robot_ask_table

== where_are_the_humans ==
Led "Do you know where the humans are?"
VacuumRobot "N-n-n-no, no humans. No humans but all their trash, al-al-always the wwwway with humans. Ev-ev-everywhere they go they leave tr-tr-trash"
Led "You don’t know either, huh? Well, maybe you know something else"
-> vacuum_robot_ask_table

== end_discussion_with_vacuum_robot ==
Led "I don’t think I have any questions"
VacuumRobot "I have tra-tra trash so much trash to cl-cl-clear"
Led "Yeah, I can see that"
VacuumRobot "There is so much so much So Much SO MUCH SO MUCH SO MUCH SOOOOOOOOOOOOO MUUUUUUUUUUUUUCH!"
Led "Are you ok?"
VacuumRobot "..."
VacuumRobot "..."
VacuumRobot "So… so much dirt. So much t-t-trash, Never ge-e-et it cleaned in time"
Led "Hey? Hello? Are you okay?"
VacuumRobot "So unclean. You-you’re sssssso dirty. Who are you?"
Led "What? It’s me, Led, remember?"
VacuumRobot "Dirty!"
Led "Drat. What could have happened to you? Something terrible. Same thing that happened to me probably. I just wish I knew what it was"
Led "Well, I’m not going to find answers with him I guess. What else is there?"
-> DONE

== data_pad ==
Led "A Data Pad. Could’ve gotten a lot out of this, but it’s cracked and flickering. Hold on…"
>> bgchange data_pad
> Hey. Word from Captain says we've got to double security
More security? Why? What's wrong? <
> I don't ask questions, man. Just get the security bots working
Led "More security? What-"
>> bgchange data_pad
Led "Damn, it's turned off. Well, that's all I'm going to get"
-> DONE

== reflection ==
>> bgchange reflection
Led "Wow. I didn't realize how damaged I was"
Led "My arm's gone, my head module is damaged- it looks like someone's attacked me?"
Led "Wait, is that rust? How long have I been here?"
>> bgchange reflection
Led "Well, staring at my reflection won't help. Let's see what else I can find"
-> DONE

== service_bot ==
Led "It must have broken apart like that what it fell over. Maybe that's what happened to me"
Led "Huh. Even its internals are damaged. What could have happened to him? And the rest of them?"
Led "They're all so dead and lifeless- but I am not. I wish I could remember..."
Led "Ug. They are too badly damaged, I can’t access them"
Led "Cold... Unmoving... So lifeless. And the rust..."
-> DONE

== all_in_one_tool ==
Led "An all-in-one tool! And it still works! Unlike my missing arm..."
Led "Wait, that robot's arm is similar to mine- and undamaged"
Led "That’s it! Maybe I can use its arm to replace my own!"
>> additem AllInOneTool
~ aquired_tool = true
-> DONE

== arm ==
>> fadeout 1
>> changesprite Led led_with_arm
>> fadein 1
Led "...Yes I have it. And it moves, perfect"
    ~ aquired_arm = true
Led "If our arms are swappable... Maybe its memory logs could be as well..."
Led "Ah, but this tool isn't sophisticated enough for that. But I'll still keep it. Just in case"
-> DONE

== horizon ==
Led "There’s... Nothing. Only worlds of ocean"
Led "I—I’ve done this before, looked up at the moon. The memory feels important but..."
Led "I can't... Ugh... I can't remember! My Memory Drive fails me"
{
    - aquired_arm:
        Led "Both arms and legs fully functioning... But I don’t feel whole"
}
Led "My memory- I need to find a way to restore it"
Led "Maybe there are answers somewhere on this ship"
-> DONE

== door ==
{
    - !stern_door_open:
        Led "There's a door over there"
        Led "I've searched everywhere out here but I still need more tools. Maybe there's something out there"
        Led "The door is bolted shut. That panel next to it looks important, but it's damaged"
}
-> DONE

== pannel ==
{
    - !aquired_tool:
        Led "The pannel to open the doors. They're too damaged to be used, but maybe I can fix it somehow"
    - !stern_door_open && aquired_tool:
        Led "I might be able to hotwire it if I get the right angle... Oh! The all-in-one tool might be able to work here!"
        {
            - aquired_arm:
                Led "If it can attach an arm, it can fix a door. It just might take a few seconds"

            - else:
                Led "But with only one arm it would be impossible to work on such a complex system"
        }
}
-> DONE

== fix_pannel ==
{
    - !aquired_arm:
        Led "I'm sure I can use this tool to fix the pannel, but not with an arm missing"
        -> DONE
}
Led "Hum, the electronic inverter isn’t re-connecting – it must have been out for a while..."
Led "I can use this wire to trip – yes. That’s got it. I just need to wait to hear it click and then –"
Led "Oh shoot, my one tool, it's all busted up! At least the door is unlocked now, I just need to reconnect this bit and..."
>> removeitem AllInOneTool
>> bgchange stern_door_open
Led "A human. First one I’ve seen since I woke up"
Led "Hello! How may I be of –Oh. Oh, you're dead. Jeeze, it looks like you've been there for a while"
Led "Poor Human. Without anyone to serve it as well"
Led "Its fingers are burned, but I can’t tell what caused its death"
Led "This Engineer’s Uniform is badly decomposed. There's a logo, but I can barely make it out"
Led "But I don't recognize it. Hm. How long have we been here for?"
~ stern_door_open = true
>> quest OpenSternDoor Completed
-> DONE

== dead_cleaning_robot ==
Led "No signs of life. Even the broom has started to rot. What could have happened to it? To us. Maybe I can check its computer banks, see if it remembers something"
Led "Hmmm... Maybe I can... No, there’s more damage than I thought. Nothing here to salvage"
Led "It must have been dead for a while, with all that rust. I wonder what its last thought was"
Led "And why is it still cleaning? Whatever happened to it- to us... Must have happened all at once"
{
    - !aquired_arm:
        Led "Hum, I wonder... I might be able to use its arm to replace mine"
        Led "Uh... It won’t budge. So many dead robots. But I am still here. Why?"
}
-> DONE

== item_use_error ==
Led "Hmmm... I can't use that here"
-> DONE

== funnel_load ==
Led "There’s so many bones- human bones. Enough for at least two fully grown ones"
Led "That worktable might have something useful on it"
~ funnel_visited = true
-> DONE

== worktable ==
Led "Useless... useless... useless. Everything is worn out and useless..."
{
    - !aquired_service_kit:
        Led "Ooh! That might not be useless"
        >> additem ServiceKit
        ~ aquired_service_kit = true
}
-> DONE

== letter ==
Unknown "... know that I love you all dearly. You're all daddy's little angels."
Unknown "And to my wife, I'm sorry it's not me who came back to you. Know that thinking of you, my darling, is all that's keeping me going."
Unknown "But I can't survive on memories alone. My love for you encompasses the universe. I hope one day, in another lifetime, we can be together again."
Unknown "And to whoever finds this letter- If you ever make it out, give this letter to my family, if you find them."
Unknown "Tell them I wish I could’ve seen them. And... if you don't get out of here, well then, the sea would’ve won."
-> DONE

== bones ==
Led "Strange- it looks like there are cuts in these bones"
Led "I don't like this... Why are there just bones? Especially when the body by the door is intact"
Led "And where are these cuts from"
Led "It looks like something or someone killed these people. And... cut them up purposefully?"
-> DONE

== funnel_data_pad ==
Led "Looks like Daily Logs from the workers. Maybe reading some will prove useful"
Unknown "Kyle, Mark and Randy here. We were working here in the Funnel Room when it happened. We got stuck. There was no way to escape..."
Led "Ugh, I can barely make out the rest of the entry"
Unknown "... Kyle died. And we just had to... We were so hungry. He would not have minded, I swear. God forgive me"
-> DONE

== clothes ==
Led "Two sets of clothing. Ripped and stained. What's it by- oh... oh no... What did you do? What happened here?"
-> DONE